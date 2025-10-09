# 请参阅 https://aka.ms/customizecontainer 以了解如何自定义调试容器，以及 Visual Studio 如何使用此 Dockerfile 生成映像以更快地进行调试。

# 此阶段用于在快速模式(默认为调试配置)下从 VS 运行时
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# 此阶段用于生成服务项目
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Warehource.Source/Warehource.Source.csproj", "Warehource.Source/"]
COPY ["Plc.Infrastructure/Plc.Infrastructure.csproj", "Plc.Infrastructure/"]
COPY ["Common.Infrastructure/Common.Infrastructure.csproj", "Common.Infrastructure/"]
COPY ["Common.Application/Common.Application.csproj", "Common.Application/"]
COPY ["Common.Domain/Common.Domain.csproj", "Common.Domain/"]
COPY ["Common.Shared/Common.Shared.csproj", "Common.Shared/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["Plc.Application/Plc.Application.csproj", "Plc.Application/"]
COPY ["Plc.Contracts/Plc.Contracts.csproj", "Plc.Contracts/"]
COPY ["Plc.Shared/Plc.Shared.csproj", "Plc.Shared/"]
COPY ["Plc.Domain/Plc.Domain.csproj", "Plc.Domain/"]
COPY ["Plc.IntegrationEvents/Plc.CustomEvents.csproj", "Plc.IntegrationEvents/"]
COPY ["Plc.Presentation/Plc.Presentation.csproj", "Plc.Presentation/"]
COPY ["Common.Presentation/Common.Presentation.csproj", "Common.Presentation/"]
COPY ["Wcs.CustomEvents/Wcs.CustomEvents.csproj", "Wcs.CustomEvents/"]
COPY ["User.Infrastructure/Identity.Infrastructure.csproj", "User.Infrastructure/"]
COPY ["User.Application/Identity.Application.csproj", "User.Application/"]
COPY ["User.Contrancts/Identity.Contrancts.csproj", "User.Contrancts/"]
COPY ["User.Domain/Identity.Domain.csproj", "User.Domain/"]
COPY ["User.Presentation/Identity.Presentation.csproj", "User.Presentation/"]
COPY ["Wcs.Infrastructure/Wcs.Infrastructure.csproj", "Wcs.Infrastructure/"]
COPY ["Wcs.Application/Wcs.Application.csproj", "Wcs.Application/"]
COPY ["Wcs.Contracts/Wcs.Contracts.csproj", "Wcs.Contracts/"]
COPY ["Wcs.Shared/Wcs.Shared.csproj", "Wcs.Shared/"]
COPY ["Wcs.Device/Wcs.Device.csproj", "Wcs.Device/"]
COPY ["Wcs.Domain/Wcs.Domain.csproj", "Wcs.Domain/"]
COPY ["Wcs.Presentation/Wcs.Presentation.csproj", "Wcs.Presentation/"]
RUN dotnet restore "./Warehource.Source/Warehource.Source.csproj"
COPY . .
WORKDIR "/src/Warehource.Source"
RUN dotnet build "./Warehource.Source.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 此阶段用于发布要复制到最终阶段的服务项目
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Warehource.Source.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 此阶段在生产中使用，或在常规模式下从 VS 运行时使用(在不使用调试配置时为默认值)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Warehource.Source.dll"]