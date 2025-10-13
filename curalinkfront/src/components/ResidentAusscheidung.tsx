import { useParams } from "react-router-dom";
import { useResident } from "../hooks/useResident";
import { useEffect, useState } from "react";
import api from "../api/axiosConfig";
import type { Ausscheidung } from "../types";

export default function ResidentAusscheidungPage() {
    const { residentId } = useParams<{ residentId: string }>();
    const { resident, loading } = useResident(residentId);
    const [ausscheidungen, setAusscheidungen] = useState<Ausscheidung[]>([]);
    const [dataLoading, setDataLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        if (!residentId) return;

        api
            .get<Ausscheidung[]>(`/api/Ausscheidung/resident/${residentId}`)
            .then((res) => {
                console.log("Ausscheidung API Response:", res.data);

                if (Array.isArray(res.data)) {
                    setAusscheidungen(res.data);
                } else {
                    console.error("Expected array but got:", typeof res.data, res.data);
                    setError("Ungültiges Datenformat");
                }
            })
            .catch((err) => {
                console.error("Error fetching ausscheidungen:", err);
                setError("Fehler beim Laden der Daten");
            })
            .finally(() => setDataLoading(false));
    }, [residentId]);

    if (loading || dataLoading) return <p>Lädt...</p>;
    if (!resident) return <p>Kein Bewohner gefunden</p>;
    if (error) return <p>{error}</p>;

    return (
        <div>
            <h2 className="text-xl font-bold mb-4">Ausscheidung</h2>
            {ausscheidungen.length === 0 ? (
                <p>Keine Daten vorhanden.</p>
            ) : (
                ausscheidungen.map((ausscheidung) => (
                    <div key={ausscheidung.id} className="border rounded p-3 mb-3 shadow">
                        <p>
                            <strong>Zeit:</strong> {new Date(ausscheidung.time).toLocaleString()}
                        </p>
                        <p><strong>Abstand:</strong> {ausscheidung.abstand}</p>
                        <p><strong>Menge:</strong> {ausscheidung.menge}</p>
                        <p><strong>Konsistenz:</strong> {ausscheidung.konsistenz}</p>
                    </div>
                ))
            )}
        </div>
    );
}