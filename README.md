# Svn日志导出

## 开始
```C#
PM> Install-Package Ionic.Fun.SvnLogExport
```
## 使用方法(代码在ConsoleTest里面)
```C#
            Config.UserName = "svn账号";
            Config.Password = "svn密码";
            Config.Repositories = new List<Model.RepositoriesModel>() {
                new Model.RepositoriesModel{
                    Name="项目1",
                    Url="https://192.168.0.11/svn/xxx/trunk"
                },
                  new Model.RepositoriesModel{
                    Name="项目2",
                    Url="https://192.168.0.11/svn/xxx/trunk"
                }

            };
            LogManager manager = new LogManager();
            var text = manager.GetCommitLog(Convert.ToDateTime("2019-02-11"), DateTime.Now).LogToText();
```
