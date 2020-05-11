# Feign4Net

a feign project corresponding to Feign(<https://github.com/OpenFeign/feign).>

## Principle

+ 所有的特性除了特定信息类的特性外，全部采用“子为准”原则。
+ URL信息相关的特性，采取“父+子”原则。
+ 默认请求body：Json字符串。
+ GET Http：
    - parameter without headerAttribute will append to query string.
+ POST 
    - Http: parameter without headerAttribute/querystringAttribute will append to post form.
    - default complex object serial：Json
+ Header/Querystring: ToString() of the value of parameter ;
+ Do not support cookie.

## Feigh4Net Attribute  

## test step

1、In the appsetting.json, http port：6346

``` cmd
cd src\TestAspNet
dotnet run
```

2、In broswer,open：<http://localhost:6346/api/student/sayhello.>  
