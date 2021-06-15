# SampleUi

本项目是一个基于  [Angular CLI](https://github.com/angular/angular-cli) 10.0.3 生成的 SPA 应用，用于配合 [ASP.NET Core 项目](https://github.com/rivenfx/SpaServices.Extensions/tree/master/tests/SampleWebApp)

## 如何运行？

本项目充当两个 SPA 应用，所以分为两个命令，单独运行

**app1**

```shell
npm run app1
```

> http://localhost:8201

**app2**

```shell
npm run app2
```

> http://localhost:8202

## 

## 如何发布？

>  注意！在发布本项目之前，应当先发布 [ASP.NET Core 项目](https://github.com/rivenfx/SpaServices.Extensions/tree/master/tests/SampleWebApp)。

本项目充当两个 SPA 应用，所以分为两个命令，单独运行

**app1**

```shell
npm run build1
```

> 发布输出所在目录为 `../SampleWebApp/bin/dist/wwwroot/app1`

**app2**

```shell
npm run build2
```

> 发布输出所在目录为 `../SampleWebApp/bin/dist/wwwroot/app2`

