# Version

## 1.0V

* 只支持字符串（xml/json/text）内容的请求体,不考虑form请求。
* 只支持GET、POST请求。
* 用于接口类的HeaderAttribute/QueryStringAttribute：
* 用于方法的HeaderAttribute/QueryStringAttribute: 只支持字符串常量，不支持动态解析求值.
* 用于参数的HeaderAttribute/QueryStringAttribute/PathParaAttribute：调用值的ToString()获取值内容。
* QueryStringAttribure不支持用于接口及方法.
  + 通过将QueryStringAttribure用于参数来实现.
* 暂时不支持复杂类型用于header、pathpara。
* 其他。
