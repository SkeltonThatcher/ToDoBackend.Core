stage('Build') {
    node {
        checkout scm
        dotnet restore
        dotnet build
    }
}

stage('Test') {
    node {
        dotnet test ToDoBackend.Core.Unit.Tests/ToDoBackend.Core.Unit.Tests.csproj --logger "trx;LogFileName=unittestresults.trx"
        mstest testResultsFile:"**/*.trx", keepLongStdio: true
    }
}