# AStore - .NET MAUI Store App

![Build Status](https://github.com/akv3sic/MAUI-store-app/workflows/Build%20.NET%20MAUI%20App%20(Android)/badge.svg)
![.NET](https://img.shields.io/badge/.NET-9.0-blue)
![Platforms](https://img.shields.io/badge/platforms-Android%20%7C%20iOS%20%7C%20Windows-blueviolet)
![Thesis Project](https://img.shields.io/badge/Based_on-Master's_Thesis-informational)
![Last Commit](https://img.shields.io/github/last-commit/akv3sic/MAUI-store-app)
![License](https://img.shields.io/github/license/akv3sic/MAUI-store-app)
![GitHub Repo stars](https://img.shields.io/github/stars/akv3sic/MAUI-store-app?style=social)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://makeapullrequest.com)

<img src="https://github.com/akv3sic/MAUI-store-app/assets/57301167/3a1a9e16-a615-49a0-8160-5d2c83345546" height="300" />
<img src="https://github.com/akv3sic/MAUI-store-app/assets/57301167/31b98a85-8b7b-4468-8706-319896e36712" height="300" />
<img src="https://github.com/akv3sic/MAUI-store-app/assets/57301167/b865cdd3-f861-46b6-af15-e6a979e66f13" height="300" />
<img src="https://github.com/akv3sic/MAUI-store-app/assets/57301167/f5a01779-ae1a-4b07-9443-da8e0498b5a6" height="300" />

## Project Description

This project is developed as a part of a Master's Thesis at [FSRE](https://fsre.sum.ba). 
It is a store application built with .NET MAUI leveraging the [Fake Store API](https://fakestoreapi.com/) to simulate
the functionality and features of a real-world store application.

You can access the full text of the thesis [here](https://drive.google.com/file/d/19TEatq-Dr9WGvYuaFw2ARENorvGu_oyr/view?usp=sharing) (Note: The document is in Croatian).

### Features

- [x] Product listing
- [x] Categories
- [x] Product details
- [x] Cart
- [x] User profile
- [x] Authentication
- [x] Sorting
- [x] Cross selling
- [x] Product sharing
- [x] Recently viewed products

### Demo Credentials
- Username: `johnd`
- Password: `m38rmF$`

More at [Fake Store API - Users](https://fakestoreapi.com/users).

## Setup Instructions

### Prerequisites

- Visual Studio 2022 17.8 or later
- .NET 9 SDK with MAUI workload installed
- Android emulator or real device with ADB enabled

### Running the app

```bash
git clone https://github.com/akv3sic/MAUI-store-app.git
cd MAUI-store-app\src
dotnet restore
dotnet build -f net9.0-android
```

## Libraries Used

The following libraries are used in this project and require attribution:

- [.NET MAUI Community Toolkit](https://github.com/CommunityToolkit/Maui)
- [MVVM Community Toolkit](https://github.com/CommunityToolkit/WindowsCommunityToolkit)

Thanks to all the contributors!

## Other Resources Used
- Empty State Illustrations by TanahAir Studio - [Figma](https://www.figma.com/community/file/931094174831888421)
- Icons by [SVG Repo](https://www.svgrepo.com/)
- App UI/UX Design inspired by E-commerce template made by Oleh Chabanov - [Behance](https://www.behance.net/gallery/107120839/Free-Mobile-AppE-commerce-templateFigmaUIStoreShop)
- This software is greatly influenced by content published by [James Montemagno](https://github.com/jamesmontemagno) and [Gerald Versluis](https://github.com/jfversluis) at their YouTube channels.
- Videos by [Javier Suárez](https://github.com/jsuarezruiz) helped me to understand how to build UI using XAML.

Thanks you for all the great resources!

## Copyright and License

© 2023–2025 A. Kvesić, mentored by Prof. J. Matković, [Faculty of Mechanical Engineering, Computing and Electrical Engineering, University of Mostar](https://fsre.sum.ba) and contributors.

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).  
Feel free to use, modify and distribute this code in accordance with the terms of the license.

Contributions are welcome and will be acknowledged under the same license.

## Thank You
A special thank you goes to my mentor and my fellow work colleagues who suggested me choosing this topic for my Master's Thesis, provided me resources and valuable feedback. I am not going to mention them by name, but they know who they are. Thank you!
