# ShortDev.Microsoft.CorrelationVector
 [![](https://img.shields.io/nuget/v/ShortDev.Microsoft.CorrelationVector.svg)](https://www.nuget.org/packages/ShortDev.Microsoft.CorrelationVector/)

Microsoft.CorrelationVector provides the CSharp implementation of the CorrelationVector protocol for tracing and correlation of events through a distributed system. 

## Background

**Correlation Vector** (a.k.a. **cV**) is a format and protocol standard for tracing and correlation of events through a distributed system based on a light weight vector clock.
The standard is widely used internally at Microsoft for first party applications and services and supported across multiple logging libraries and platforms (Services, Clients - Native, Managed, Js, iOS, Android etc). The standard powers a variety of different data processing needs ranging from distributed tracing & debugging to system and business intelligence, in various business organizations.

For more on the correlation vector specification and the scenarios it supports, please refer to the [specification](https://github.com/Microsoft/CorrelationVector) repo.

## License
This project is licensed under the [MIT License](LICENSE).
