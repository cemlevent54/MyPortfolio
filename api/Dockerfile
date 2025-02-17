# Build aşaması
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Çözüm dosyasını (sln) ve proje dosyasını (csproj) kopyalayarak restore işlemi yap
COPY *.sln ./
COPY *.csproj ./
RUN dotnet restore

# Tüm kaynak dosyalarını kopyala ve derleme işlemini gerçekleştir
COPY . ./
RUN dotnet build -c Release -o /app/build

# Yayın (publish) işlemini gerçekleştir
RUN dotnet publish -c Release -o /app/publish --no-restore

# Final aşaması (runtime image)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Build aşamasından çıkan publish dosyalarını kopyala
COPY --from=build /app/publish .

# Copy SQLite database file into the container
COPY MyPortfolio.db /app/

# Veritabanı dosyasına izin ver (gerekirse)
RUN chmod 777 /app/MyPortfolio.db

# Portu aç (API'nin dış dünya ile iletişim kurması için)
EXPOSE 80

# Dotnet SDK'yı runtime'a ekleyelim (dotnet watch çalıştırmak için)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS watch
WORKDIR /app
COPY --from=runtime /app /app



# Uygulamayı çalıştırmak için dotnet watch kullan
ENTRYPOINT ["dotnet", "watch", "run", "--urls", "http://*:80"]
