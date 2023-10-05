# Instructions to run micro-services
## Run following commands in the given order after navigating to current location

	docker-compose build
	docker-compose up

## This will build and start the micro services.
## And expose the endpoint throuth following baseUrl
## This baseUrl will be used by the mobile and web clients to send REST api requests

	baseUrl : http://192.168.1.4:5000

## NOTE
## Change the above ip address in Orchestrator/ocelot.json to your host machine's ip address.
## You can find it by typing 'ifconfig' on your terminal
