import api from "../api/axiosConfig";

export interface Ausscheidung {
    id: number;
    residentId: number;
    staffId: number;
    time: string;
    abstand: string;
    menge: string;
    konsistenz: string;
}

export async function getAusscheidungen(residentId: number): Promise<Ausscheidung[]> {
    const response = await api.get(`/Ausscheidungen/${residentId}`);
    return response.data;
}
