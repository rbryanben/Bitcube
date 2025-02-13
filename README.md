# Bitcube 
Task for the Bitcube development interview process. This **README.md** describes how to run the project and interract with the RESTful service for user management, product operations, cart functionalities, and checkout processes.

# Setup Using Docker
1. Lets start by cloning the repository and move into it:  
   ```sh
   git clone https://github.com/rbryanben/Bitcube.git
   cd Bitcube
   ```
2. Build the docker image:
    ```sh
    docker build -t bitcube:latest .
    ```
3. Run the image and bind to your desired host port:
    ```sh
    docker run -d --name sandy-milk -p <host-port>:80 bitcube
    ```
4. Test the service by sending a curl request to create a new product:
    ```curl
    curl --location 'http://localhost:8080/api/v1/add-product' \
    --header 'x-api-key: cc99b58f-5abe-491b-ad81-7ca9f78b52b126dce7ef-a579-4d6e-97db-880d07984b46' \
    --header 'Content-Type: application/json' \
    --data '{
        "product_id" : "ZA002",
        "product_name" : "Fossil Machine Black Stainless Steel Watch - FS4552",
        "quantity" : 65,
        "product_price" : 3999
    }'
    ```
 
    We should receive the following response 
    ```json
    {
        "product_id": "ZA002",
        "owner": "bitcube"
    }
    ```

## Docker 

## Docker Is The Way To Go 
Docker 
## 🚀 Setting Up Postman  
 For interaction with the Rest API, and easy way to get started is to the postman collection and environment that is in the the <a href="./postman">postman directory</a>
 </br>
I have created a default API key that belongs to the user **bitcube** with the following api key.
</br></br>
    ```cc99b58f-5abe-491b-ad81-7ca9f78b52b126dce7ef-a579-4d6e-97db-880d07984b46
    ```


