import api from "./api";

export async function getMealSchedules(residentId: number) {
    const response = await api.get(`/MealSchedules/${residentId}`);
    return response.data;
}
