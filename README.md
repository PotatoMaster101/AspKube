# ASP.NET and Kubernetes
A sample ASP.NET core application to play around with Kubernetes.

## Deploying (`docker compose`)
Generate a HTTPS dev cert under `~/.aspnet/https` (hardcoding password here but who cares... it's a dev cert):
```
$ dotnet dev-certs https -ep "$HOME/.aspnet/https/auth.pfx" -p Password123!
$ dotnet dev-certs https --trust
```

Deploy using `docker compose`:
```
$ docker compose up -d --build
```

Verify site/db containers are running:
```
$ docker ps
```
Visit `https://localhost` to verify site is up.

### Shutdown
```
$ docker compose down
```

## Deploying (`kubectl`)
Build the docker image locally (optional - automatically pulls if not present):
```
$ docker build . -t potatomaster101/aspkube:latest --no-cache
```

Deploy onto local Kubernetes:
```
$ kubectl apply -f .k8s/db/
$ kubectl apply -f .k8s/web/
```

Verify deployments/pods are up:
```
$ kubectl get pods
$ kubectl get deployments
```
Visit `http://localhost` to verify site is up.

### Shutdown
```
$ kubectl delete -f .k8s/db/
$ kubectl delete -f .k8s/web/
```

## Endpoints
- `/`: prints `It Works!` if site is up
- `/shutdown`: shuts down the webapp, used to test Kubernetes auto restart
- `/db`: connects to DB container and grab the version, used to verify DB connection is good

## TODO
- Configure HTTPS for Kubernetes
