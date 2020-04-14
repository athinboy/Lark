# Feign4Net

a feign project corresponding to Feign(https://github.com/OpenFeign/feign).

## Principle

+ 所有的特性除了特定信息类的特性外，全部采用“子为准”原则。
+ URL信息相关的特性，采取“父+子”原则。
+ 默认序列化方式：Json。
+ 默认请求body：Json字符串。

## Feigh4Net Attribute

## Supported Asp MVC Attribute

+ FromFormAttribute
+ FromBodyAttribute
+ FromHeaderAttribute
+ FromQueryAttribute
+ HttpDeleteAttribute
+ HttpGetAttribute
+ HttpHeadAttribute  
+ HttpPostAttribute
+ HttpPutAttribute

## 测试步骤

1、appsetting.json中已经配置端口：6346

``` cmd
cd src\TestAspNet
dotnet run
```

2、浏览器打开：http://localhost:6346/api/student/sayhello