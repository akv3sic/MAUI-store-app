

# AStore - Aplicación de Tienda .NET MAUI

![Estado de compilación](https://github.com/akv3sic/MAUI-store-app/workflows/Build%20.NET%20MAUI%20App%20(Android)/badge.svg)
![.NET](https://img.shields.io/badge/.NET-9.0-blue)
![Plataformas](https://img.shields.io/badge/platforms-Android%20%7C%20iOS%20%7C%20Windows-blueviolet)
![Proyecto de Tesis](https://img.shields.io/badge/Based_on-Master's_Thesis-informational)
![Último Commit](https://img.shields.io/github/last-commit/akv3sic/MAUI-store-app)
![Licencia](https://img.shields.io/github/license/akv3sic/MAUI-store-app)
![Estrellas del repositorio](https://img.shields.io/github/stars/akv3sic/MAUI-store-app?style=social)
[![PRs Bienvenidos](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://makeapullrequest.com)

<img src="https://github.com/akv3sic/MAUI-store-app/assets/57301167/3a1a9e16-a615-49a0-8160-5d2c83345546" height="300" />
<img src="https://github.com/akv3sic/MAUI-store-app/assets/57301167/31b98a85-8b7b-4468-8706-319896e36712" height="300" />
<img src="https://github.com/akv3sic/MAUI-store-app/assets/57301167/b865cdd3-f861-46b6-af15-e6a979e66f13" height="300" />
<img src="https://github.com/akv3sic/MAUI-store-app/assets/57301167/f5a01779-ae1a-4b07-9443-da8e0498b5a6" height="300" />

## Descripción del Proyecto

Este proyecto se ha desarrollado como parte de una Tesis de Máster en la [FSRE](https://fsre.sum.ba). 
Es una aplicación de tienda construida con .NET MAUI que aprovecha la [API Fake Store](https://fakestoreapi.com/) para simular
la funcionalidad y características de una aplicación de tienda del mundo real.

Puedes acceder al texto completo de la tesis [aquí](https://drive.google.com/file/d/19TEatq-Dr9WGvYuaFw2ARENorvGu_oyr/view?usp=sharing) (Nota: El documento está en croata).

### Características

- [x] Listado de productos
- [x] Categorías
- [x] Detalles del producto
- [x] Carrito
- [x] Perfil de usuario
- [x] Autenticación
- [x] Ordenación
- [x] Venta cruzada
- [x] Compartir productos
- [x] Productos vistos recientemente

### Credenciales de Demostración
- Nombre de usuario: `johnd`
- Contraseña: `m38rmF$`

Más información en [Fake Store API - Usuarios](https://fakestoreapi.com/users).

## Instrucciones de Configuración

### Requisitos previos

- Visual Studio 2022 17.8 o posterior
- SDK de .NET 9 con la carga de trabajo (workload) de MAUI instalada
- Emulador de Android o dispositivo real con ADB habilitado

### Ejecutar la aplicación

```bash
git clone https://github.com/akv3sic/MAUI-store-app.git
cd MAUI-store-app\src
dotnet restore
dotnet build -f net9.0-android
```

## Bibliotecas Utilizadas

Las siguientes bibliotecas se utilizan en este proyecto y requieren atribución:

- [.NET MAUI Community Toolkit](https://github.com/CommunityToolkit/Maui)
- [MVVM Community Toolkit](https://github.com/CommunityToolkit/WindowsCommunityToolkit)

¡Gracias a todos los colaboradores!

## Otros Recursos Utilizados
- Ilustraciones de estado vacío por TanahAir Studio - [Figma](https://www.figma.com/community/file/931094174831888421)
- Iconos por [SVG Repo](https://www.svgrepo.com/)
- Diseño de UI/UX de la aplicación inspirado en la plantilla de comercio electrónico creada por Oleh Chabanov - [Behance](https://www.behance.net/gallery/107120839/Free-Mobile-AppE-commerce-templateFigmaUIStoreShop)
- Este software está fuertemente influenciado por el contenido publicado por [James Montemagno](https://github.com/jamesmontemagno) y [Gerald Versluis](https://github.com/jfversluis) en sus canales de YouTube.
- Los videos de [Javier Suárez](https://github.com/jsuarezruiz) me ayudaron a comprender cómo construir interfaces de usuario utilizando XAML.

¡Gracias por todos los excelentes recursos!

## Copyright y Licencia

© 2023–2026 A. Kvesić, bajo la mentoría del Prof. J. Matković, [Facultad de Ingeniería Mecánica, Informática y Eléctrica, Universidad de Mostar](https://fsre.sum.ba) y colaboradores.

Este proyecto está licenciado bajo la [Licencia MIT](https://opensource.org/licenses/MIT).  
Siéntete libre de usar, modificar y distribuir este código de acuerdo con los términos de la licencia.

Las contribuciones son bienvenidas y serán reconocidas bajo la misma licencia.

## Agradecimientos
Un agradecimiento especial a mi mentor y a mis compañeros de trabajo que me sugirieron elegir este tema para mi Tesis de Máster, me proporcionaron recursos y valiosos comentarios. No los mencionaré por su nombre, pero saben quiénes son. ¡Gracias!
