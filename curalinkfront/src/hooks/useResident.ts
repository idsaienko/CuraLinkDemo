import { useEffect, useState } from "react";
import api from "../api/axiosConfig";
import type { Resident } from "../types";

export function useResident(residentId?: string) {
    const [resident, setResident] = useState<Resident | null>(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        if (!residentId) return;

        setLoading(true);
        api
            .get<Resident>(`/api/residents/${residentId}`)
            .then((res) => setResident(res.data))
            .catch((err) => console.error("Error fetching resident:", err))
            .finally(() => setLoading(false));
    }, [residentId]);

    return { resident, loading };
}