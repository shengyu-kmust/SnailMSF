# SnailMSF
snail microservice framework

## 用到的技术栈
* 网关的选择：nginx,envoy,ocelot等
* docker的编排：docker compose(单主机),k8s
* 结构化日志处理：serrilog、seq、logstash
* 配置：azureKeyVault,json file,env
* api:同时向外提供webapi和grpc
* api文档：swaggerr，继承auth2.0
* 身份验证，统一为identityapi
* 事件：rabbitmq
* di：autofac
* 接口聚合：
* data protection
* 日志处理，[分布式日志](https://opentelemetry.io/docs/)，elk，结构化日志，[serilog](https://serilog.net/)
* clean architecture
* dataaccess repository pattern,ef and dapper
* dapr或是service mesh技术?
* gRPC
## 结构说明
--IdentityServer   // identity server microservice
--Demo.Api  // 微服务示例
--ServiceCommon // 各微服务之间公用