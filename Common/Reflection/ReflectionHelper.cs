using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;

namespace Common.Reflection;

/// <summary>
///     加载程序所有引用的非systemdll文件，
/// </summary>
public static class ReflectionHelper
{
    private static bool IsSystemAssembly(Assembly asm)
    {
        var asmCompanyAttr = asm.GetCustomAttribute<AssemblyCompanyAttribute>();
        if (asmCompanyAttr == null) return false;

        var companyName = asmCompanyAttr.Company;
        return companyName.Contains("Microsoft");
    }

    private static Assembly? TryLoadAssembly(string pathName)
    {
        var assembly = AssemblyName.GetAssemblyName(pathName);
        Assembly? asm = null;
        try
        {
            asm = Assembly.Load(assembly);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }

        if (asm == null)
            try
            {
                asm = Assembly.Load(pathName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        return asm;
    }

    private static bool IsValid(Assembly asm)
    {
        try
        {
            asm.GetTypes();
            asm.DefinedTypes.ToList();
            return true;
        }
        catch (ReflectionTypeLoadException)
        {
            return false;
        }
    }

    /// <summary>
    ///     判断file这个文件是否是程序集
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    private static bool IsManagedAssembly(string file)
    {
        using var fs = System.IO.File.OpenRead(file);
        using var peReader = new PEReader(fs);
        return peReader.HasMetadata && peReader.GetMetadataReader().IsAssembly;
    }

    public static IEnumerable<Assembly> GetAllReferencedAssemblies(bool skipSystemAssemblies = true)
    {
        //程序集运行的dll
        var rootAsm = Assembly.GetEntryAssembly();
        if (rootAsm == null)
            rootAsm = Assembly.GetCallingAssembly();
        var returnAsms = new HashSet<Assembly>(new AssemblyEquality());
        var pathAsms = new HashSet<string>();
        var asmsChecks = new Queue<Assembly>();
        asmsChecks.Enqueue(rootAsm);
        if (skipSystemAssemblies && !IsSystemAssembly(rootAsm))
            if (IsValid(rootAsm))
                returnAsms.Add(rootAsm);
        while (asmsChecks.Any())
        {
            var asm = asmsChecks.Dequeue();
            //该程序集所关联的所有dll
            foreach (var item in asm.GetReferencedAssemblies())
                if (!pathAsms.Contains(item.FullName))
                {
                    var ay = Assembly.Load(item);
                    if (skipSystemAssemblies && IsSystemAssembly(ay))
                        continue;
                    pathAsms.Add(item.FullName);
                    //因为需要该dll依赖的dll可能还依赖其他dll
                    asmsChecks.Enqueue(asm);
                    if (IsValid(ay))
                        returnAsms.Add(asm);
                }
        }

        //获取程序运行的文件下所有的dll文件
        var asmPath = Directory.EnumerateFiles(AppContext.BaseDirectory, "*.dll",
            new EnumerationOptions { RecurseSubdirectories = true });
        foreach (var path in asmPath)
        {
            if (!IsManagedAssembly(path))
                continue;
            var asmName = AssemblyName.GetAssemblyName(path);
            if (pathAsms.Any(p => p.Contains(asmName.FullName)))
                continue;
            var asm = TryLoadAssembly(path);
            if (skipSystemAssemblies && IsSystemAssembly(asm))
                continue;
            if (!IsValid(asm))
                continue;
            returnAsms.Add(asm);
            pathAsms.Add(asmName.FullName);
        }

        return returnAsms.ToArray();
    }
}