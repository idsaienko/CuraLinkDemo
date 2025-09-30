import axios from "axios";

const api = axios.create({
    baseURL: "http://localhost:57378/api",
});

export const getResident = async (residentId: number) => {
    const res = await axios.get(`${api}/Residents/${residentId}`);
    return res.data;
};

export const getMealSchedules = async (residentId: number) => {
    const res = await axios.get(`${api}/MealSchedules/${residentId}`);
    return res.data;
};

export const getMovements = async (residentId: number) => {
    const res = await axios.get(`${api}/Movements/${residentId}`);
    return res.data;
};

export default api;