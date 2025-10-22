import { useParams } from "react-router-dom";
import { useResident } from "../hooks/useResident";
import { useEffect, useState } from "react";
import api from "../api/axiosConfig";
import type { ResidentMovement } from "../types";

export default function ResidentMobilityPage() {
    const { residentId } = useParams<{ residentId: string }>();
    const { resident, loading } = useResident(residentId);
    const [movements, setMovements] = useState<ResidentMovement[]>([]);
    const [dataLoading, setDataLoading] = useState(true);

    useEffect(() => {
        if (!residentId) return;

        api
            .get<ResidentMovement[]>(`http://localhost:5043/api/ResidentMovements/${residentId}`)
            .then((res) => setMovements(res.data))
            .catch((err) => console.error("Error fetching movements:", err))
            .finally(() => setDataLoading(false));
    }, [residentId]);

    if (loading || dataLoading) return <p>Lädt...</p>;
    if (!resident) return <p>Kein Bewohner gefunden</p>;

    return (
        <div style={{color:"black"} }>
            <h2 className="text-xl font-bold mb-4">Mobilität von {resident.fullName}</h2>
            {movements.length === 0 ? (
                <p>Keine Daten vorhanden.</p>
            ) : (
                <ul className="space-y-3">
                    {movements.map((movement) => (
                        <li key={movement.id} className="border rounded p-3 shadow">
                            <p>
                                <strong>Zeit:</strong>{" "}
                                {new Date(movement.movementTime).toLocaleString()}
                            </p>
                            <p><strong>Raum:</strong> {movement.room}</p>
                            <p><strong>Objekt:</strong> {movement.object}</p>
                            <p><strong>Winkel:</strong> {movement.angle}°</p>
                            <p><strong>Notizen:</strong> {movement.notes || 'Keine Notizen'}</p>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}