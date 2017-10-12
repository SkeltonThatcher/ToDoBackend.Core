# ToDoBackend.Core
## _Example application for the Releasability Book_

The app is intended to be a solution to the [ToDoBackend](https://www.todobackend.com) scenario.

For seeing the system of delivery in action, the Jenkins CI servers and GoCD can be run locally as Docker containers.

## Running the Docker Containers

### Jenkins CI server
For the full Jenkins docker container documentation head to the repo for the [official Jenkins Docker image](https://github.com/jenkinsci/docker/blob/master/README.md).

To ensure that you don't lose any settings or customisations, mount a volume tied to a local directory.

    mkdir jenkins_home

    docker run -d --name _myjenkins_ -p 8080:8080 -p 50000:50000 -v `pwd`/jenkins_home:/var/jenkins_home jenkins/jenkins:lts

Once complete, the server can be accessed at http://localhost:8080

To install .net core we need to run bash on the container as root and follow the [official instructions](https://www.microsoft.com/net/core#linuxdebian)

    docker exec -ti -u 0 _myjenkins_ bash

For the test results to be correctly interpreted, you'll need to enable the [MSTest Plugin](https://wiki.jenkins.io/display/JENKINS/MSTest+Plugin)

### GoCD Server
For the full GoCD docker container documentation head to the [Docker Hub page](https://hub.docker.com/r/gocd/gocd-server/).

To retain the pipeline history, mount a volume pointing `/godata` local directory.  Mounting `/home/go` allows us to supply SSH credentials.

    mkdir godata
    mkdir go_home

    docker run -d --name mygocd -p8153:8153 -p8154:8154 -v `pwd`/godata:/godata -v `pwd`/go_home:/home/go gocd/gocd-server:v17.10.0

Once complete, the server can be accessed at http://localhost:8153
