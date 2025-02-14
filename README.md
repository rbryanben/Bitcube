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
# Getting Started - Rest API 
An easy way to get started is to import the postman collection and enviroment <a href="/environment">files</a> into your postman environment.<br>
To quickly get started I have create a user **bitcube** with the following **X-API_KEY** : <br><br>
```cc99b58f-5abe-491b-ad81-7ca9f78b52b126dce7ef-a579-4d6e-97db-880d07984b46```<br><br>
You have already used this key when you were testing if the service is working as intended, that was on creating a product.<br>
To create your own key referer to the **User Management** section which follows after this.

## User Management - Create User
Create a new user by sending the below curl request. Parameters `firstname`, `lastname` and `username` are required, and each `username` should be unique.
```curl
curl --location 'http://localhost:8080/api/v1/create-user' \
--header 'x;' \
--header 'Content-Type: application/json' \
--data '{
   "firstname" : "Ryan",
   "lastname" : "Ben",
   "username" : "rbryanben"
}'
```
200 - Response 
```json 
{
    "username": "rbryanben",
    "apiKey": "568ac8d4-1d67-4472-92c8-55629b9e19af90d3d927-2389-4deb-b5a1-199304207018"
}
```
## Product Management - Add Product
Create a new product by sending the below curl request. Parameters `product_id` and `product_name` are mandatory.
Parameters `quantity` and `product_price` will default to zero if not supplied. Each `product_id` should be unique. 
Authorization is required for this endpoint and you should provide your api key as `x-api-key` header parameter.
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
200 - Response 
```json 
{
    "product_id": "ZA002",
    "owner": "rbryanben"
}
```
## Product Management - View Products
View all products by sending the below curl request. Parameters `count` and `index` are optional and can be used to set the number of items to return starting from index.
Authorization is required for this endpoint and you should provide your api key as `x-api-key` header parameter.
```curl
curl --location 'http://localhost:8080/api/v1/get-products' \
--header 'x-api-key: cc99b58f-5abe-491b-ad81-7ca9f78b52b126dce7ef-a579-4d6e-97db-880d07984b46'
```
200 - Response 
```json 
[
    {
        "id": 1,
        "product_id": "ZA002",
        "product_name": "Fossil Machine Black Stainless Steel Watch - FS4552",
        "price": 3999,
        "owner": "bitcube",
        "quantity": 65,
        "created": "2025-02-13T09:22:17.9149075",
        "last_update": "2025-02-13T09:22:17.9149278"
    }
]
```
## Product Management - Delete Products
Delete a product by sending the below request to the service. Take note that only the owner of a product can delete their product.
Authorization is required for this endpoint and you should provide your api key as `x-api-key` header parameter.
```curl
curl --location --request DELETE 'http://localhost:8080/api/v1/delete-product?product_id=ZA002' \
--header 'x-api-key: cc99b58f-5abe-491b-ad81-7ca9f78b52b126dce7ef-a579-4d6e-97db-880d07984b46'
```
200 - Response 
```curl
ZA002
```
## Product Management - Modify Product
Modify a product by sending the below request to the service. Take note that only the owner of a product can modify their product. Parameters `product_id`, `product_name` are mandatory, where parameters `quantity` and `product_price` will default to zero if not supplied.
Authorization is required for this endpoint and you should provide your api key as `x-api-key` header parameter.
```curl
curl --location --request PUT 'http://localhost:8080/api/v1/modify-product' \
--header 'x-api-key: cc99b58f-5abe-491b-ad81-7ca9f78b52b126dce7ef-a579-4d6e-97db-880d07984b46' \
--header 'Content-Type: application/json' \
--data '{
    "product_id" : "ZA002",
    "product_name" : "HP Zenbook 2022",
    "quantity" : 55,
    "product_price" : 499.99
}'
```
200 - Response 
```json
{
    "new_object": {
        "productId": "ZA002",
        "productName": "HP Zenbook 2022",
        "productPrice": 499.99,
        "quantity": 55,
        "createdBy": "bitcube",
        "created": "2025-02-13T09:58:19.5603173+00:00",
        "last_updated": "2025-02-13T09:58:19.5603226+00:00"
    },
    "old_object": {
        "productId": "ZA002",
        "productName": "Fossil Machine Black Stainless Steel Watch - FS4552",
        "productPrice": 3999,
        "quantity": 65,
        "createdBy": "bitcube",
        "created": "2025-02-13T09:58:19.5608587+00:00",
        "last_updated": "2025-02-13T09:58:19.5608618+00:00"
    }
}
```
## Checkout - Add To Cart / Modify Cart
Add products to your cart by sending a list of products to add as the below curl request. If the user does not have a cart one will be created. A cart can either be open or closed and will be open on creation and will close on checkout (not user managed). A user can only have one open cart at a time. If a user has an open cart and  product to add already exists in the cart, then the product in the cart will be overriden.
Authorization is required for this endpoint and you should provide your api key as `x-api-key` header parameter.
```curl
curl --location 'http://localhost:8080/api/v1/add-to-cart' \
--header 'x-api-key: cc99b58f-5abe-491b-ad81-7ca9f78b52b126dce7ef-a579-4d6e-97db-880d07984b46' \
--header 'Content-Type: application/json' \
--data '{
    "products" : [
        {
            "product_id" : "ZA001",
            "quantity" : 1
        },
         {
            "product_id" : "ZA002",
            "quantity" : 2
        }
    ]
}'
```
The response will be a list of the same products that were requested and the status of the products in the cart. The status could either be `PRODUCT_NOT_FOUND`, `NOT_ENOUGH_STOCK` or `SUCCESS`
```json
[
    {
        "product_id": "ZA001",
        "status": "PRODUCT_NOT_FOUND",
        "item_price": 0,
        "total_price": 0,
        "quantity_in_cart": 0
    },
    {
        "product_id": "ZA002",
        "status": "SUCCESS",
        "item_price": 499.99,
        "total_price": 999.98,
        "quantity_in_cart": 2
    }
]
```
## Checkout - View Cart
View your cart by sending the below curl request.
Authorization is required for this endpoint and you should provide your api key as `x-api-key` header parameter.
```curl
curl --location 'http://localhost:8080/api/v1/cart' \
--header 'x-api-key: cc99b58f-5abe-491b-ad81-7ca9f78b52b126dce7ef-a579-4d6e-97db-880d07984b46'
```
200 - Response 
```json
[
    {
        "product_id": "ZA003",
        "product_name": "Fossil Machine Black Stainless Steel Watch - FS4552",
        "product_price": 3999,
        "quantity": 1,
        "subtotal": 3999,
        "cart_ref": "7cb2b540-6e1a-4443-9973-c965d09c50f3"
    },
    {
        "product_id": "ZA002",
        "product_name": "HP Zenbook 2022",
        "product_price": 499.99,
        "quantity": 2,
        "subtotal": 999.98,
        "cart_ref": "7cb2b540-6e1a-4443-9973-c965d09c50f3"
    }
]
```
## Checkout - Complete Checkout
Complete your checkout by sending the below curl request. Upon submission the quanties of products will be checked against the ones in stock. If any of the products are not in stock then the entire checkout will fail.
Authorization is required for this endpoint and you should provide your api key as `x-api-key` header parameter.
```curl
curl --location 'http://localhost:8080/api/v1/checkout' \
--header 'x-api-key: cc99b58f-5abe-491b-ad81-7ca9f78b52b126dce7ef-a579-4d6e-97db-880d07984b46' \
--data ''
```
409 - On Error 
```json
[
    {
        "product_id": "ZA002",
        "error": "NOT_ENOUGH_STOCK",
        "in_stock": 0,
        "required": 2
    }
]
```
