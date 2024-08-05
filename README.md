# Backend Eventos CAS

Este es un proyecto en C# y SQL que utiliza el framework .NET 7 y Entity Framework Core para operaciones de base de datos. El proyecto está configurado con autenticación JWT y una política CORS para orígenes específicos.

## Comenzando

Estas instrucciones te permitirán obtener una copia del proyecto y ejecutarlo en tu máquina local para propósitos de desarrollo y pruebas, este backend permite el guardado de los datos de las entidades y alberga la logica de la aplicación.

### Prerrequisitos

- SDK de .NET 7
- SQL Server
- JetBrains Rider 2024.1.1 o cualquier otro IDE compatible
- Docker (para ejecutar la base de datos en MacOS y/o Linux)

## Ejecutar el Backend con Base de Datos en Docker (si usas MacOS y/o Linux)

1. **Ejecuta el archivo de docker-compose**.

    ```bash
    docker-compose up -d
    ```

2. **Obtén el ID del contenedor**.

    ```bash
    docker ps
    ```

3. **Ejecuta el siguiente comando para crear tablas**.

    ```bash
    docker exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong@Passw0rd -i ./init/QueryBackendCas.sql
    ```

### Instalación

1. **Clona el repositorio** en tu máquina local.
2. **Abre la solución** en JetBrains Rider o tu IDE preferido.
3. **Actualiza las cadenas de conexión** en `appsettings.json` y `appsettings.Development.json` para que coincidan con tu instancia de SQL Server.
4. **Ejecuta la aplicación**.

## Autores

- Cristina Lourdes Vidal Falcon
- Paolo Andre Espejo Macuri
- Walter Emilio Delgsdo Yucra
- Eric Fernando Cuevas Rios

## Licencia

MIT License
Copyright (c) 2024 IEEE-CAS-UPC

## Contacto  

Linkedin: https://www.linkedin.com/company/ieee-cas-upc  
Instagram: https://www.instagram.com/ieee.cas.upc/  
Linktr: https://linktr.ee/ieee.cas.upc   
