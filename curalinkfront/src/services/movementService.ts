import api from "../api/axiosConfig";

export interface Movement {
    id: number;
    residentId: number;
    staffId: number;
    movementTime: string;
    room: string;
    object: string;
    angle: number;
    notes: string;
}

export async function getMovements(residentId: number): Promise<Movement[]> {
    const response = await api.get(`/Movements/${residentId}`);
    return response.data;
}
