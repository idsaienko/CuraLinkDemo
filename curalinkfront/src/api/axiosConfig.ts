import axios from "axios";

const api = axios.create({
    baseURL: process.env.NODE_ENV === 'production'
        ? "https://mybackend-api-curalink.azurewebsites.net"  // Your Azure URL
        : "http://localhost:5043",
    headers: {
        'X-API-Key': 'your-api-key-here'  // Replace with actual key
    }
});

console.log("Axios baseURL configured as:", api.defaults.baseURL);

export default api;