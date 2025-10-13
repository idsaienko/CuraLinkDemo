import api from "./api";

export async function getMealSchedules() {
    const response = await api.get(`/MealSchedules/`);
    return response.data;
}
