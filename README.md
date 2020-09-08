# BsCApp 

BscApp is an application designed to provide users with the ability to find cheap flights to many destinations around the world and make ticket reservations for them. They are also able to find and rent a vehicle during the trip from many rent-a-car companies. After the trip, users can rate the services and help others to make the best choice!

The app is contained of 2 parts: 

1) Frontend part designed in Angular framework 
2) Backend part (.NET Core WebApi, Sql server)

The backend part is splitted into 4 microservices.

## Installation of the Frontend part

1. Run the `npm install` to install all the dependecies required to run the project.

2. Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Installation of the Backend part

### Option 1: Using Docker-compose

In order to run the back-end you need to have Docker Desktop installed on your machine. Make sure you have VM virutalization enabled and Docker Desktop running. Navigate to the root application folder and run:

```bash
docker-compose up
```

Next, open the environment.ts file in the SPA and make changes to the endpoints replacing the code with the following: 

```javascript
export const environmnet = { 
   production: false, 
   carUrl: 'http://localhost:5001/api/'
   avioUrl: 'http://localhost:5000/api/'
   authUrl: 'http://localhost:5002/api/'
};

```

### Option 2: Using Kubernetes

In order to run the back-end you need to have Docker Desktop installed on your machine. Make sure you have VM virutalization enabled and Docker Desktop running with Kubernetes enabled. Navigate to the root application folder and run:

```bash
kubectl apply -f auth-api-deployment.yml, avio-api-deployment.yml, car-api-deployment.yml, ingress-rules.yml
```

### Please note! 

#### You need to have Nginx Ingress controller already set up in order to apply Ingress rules. If you want to run the app without an Load Balancer proxy, you need to change service type  from ClusterIP to NodePort (or LoadBalancer) for each microservice. If that is the case, you will need to make the same changes to the SPA part as desribed in the Option 1.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://github.com/AndrejKaravida/Bachelors-microservices/blob/master/LICENSE)
