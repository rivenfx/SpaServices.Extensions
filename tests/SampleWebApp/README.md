# SampleWebApp

本项目是一个 Razor Page 的 Web 应用，运行后可以通过 `http://localhost:5000/` 访问。 

并且在启动 SPA 项目后可以通过 `http://localhost:5000/app1` 与 `http://localhost:5000/app2` 访问到对应的 SPA 应用。


[SPA项目地址](https://github.com/rivenfx/SpaServices.Extensions/tree/master/tests/sample-ui)

## 如何运行？
使用vs直接运行或通过命令 `dotnet run` 运行


## 如何发布？

1. 运行 `dotnet build -c Release -o bin\dist`

2. [查看 SPA 项目文档关于发布的部分](https://github.com/rivenfx/SpaServices.Extensions/tree/master/tests/sample-ui)
