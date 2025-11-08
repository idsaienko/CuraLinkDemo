import api from "../api/axiosConfig";

export async function getMealSchedules() {
    const response = await api.get(`/MealSchedules/`);
    return response.data;
}
