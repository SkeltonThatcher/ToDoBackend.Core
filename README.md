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

To install .net core we need to run bash on the container as root and follow the [official instructions](https://www.microsoft.com/net/learn/get-started/linux/debian9)

    docker exec -ti -u 0 _myjenkins_ bash

For the test results to be correctly interpreted, you'll need to enable the [MSTest Plugin](https://wiki.jenkins.io/display/JENKINS/MSTest+Plugin)

### GoCD Server
For the full GoCD docker container documentation head to the [Docker Hub page](https://hub.docker.com/r/gocd/gocd-server/).

To retain the pipeline history, mount a volume pointing `/godata` local directory.  Mounting `/home/go` allows us to supply SSH credentials.

    mkdir godata
    mkdir go_home

    docker run -d --name mygocd -p8153:8153 -p8154:8154 -v `pwd`/godata:/godata -v `pwd`/go_home:/home/go gocd/gocd-server:v17.10.0

Once complete, the server can be accessed at http://localhost:8153

#### Install the Json Configuration Plugin
By default GoCD only supports pipelines defined in XML.  To enable Json support you'll need to install the [Json Configuration plugin](https://github.com/tomzo/gocd-json-config-plugin).  

Plugins are installed by dropping [the jar](https://github.com/tomzo/gocd-json-config-plugin/releases) into the `/godata/plugins/external` directory which should be mapped to a local directory as per the above commands.  Restart the docker container:

    docker restart mygocd

#### Start a GoCD Agent
Unlike Jenkins, the GoCD server does not have the ability to process tasks internally and requires an agent.  This is available another [docker image](https://hub.docker.com/r/gocd/gocd-agent-ubuntu-16.04/).

To register the agent with the server we need the server's `agentAutoRegisterKey` which is found in the `config.xml`.

This command will grab the ip address of the gocd server if named `mygocd`, set the agent auto register key and run the container:

    docker run -itd -e GO_SERVER_URL=https://$(docker inspect --format='{{(index (index .NetworkSettings.IPAddress))}}' mygocd):8154/go -e AGENT_AUTO_REGISTER_KEY=d87eef50-43e5-4846-b693-cdbe0b50a05c gocd/gocd-agent-ubuntu-16.04:v17.10.0
