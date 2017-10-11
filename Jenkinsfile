stage('Build') {
    node {
        checkout scm
        sh 'dotnet restore'
        sh 'dotnet build'
    }
}

stage('Test') {
    node {
        sh 'dotnet test ToDoBackend.Core.Unit.Tests/ToDoBackend.Core.Unit.Tests.csproj --logger "trx;LogFileName=unittestresults.trx"'
        mstest testResultsFile:"**/*.trx", keepLongStdio: true
    }
}