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
        <div className = "AusscheidungPageWrap">
            <h2 className="AusscheidungTitel">Ausscheidung</h2>
            <div className="AusscheidungWrap">
                <p className = "ster">Zeit:</p>
                <p className= "ster">Abstand:</p>
                <p className= "ster">Menge:</p>
                <p className= "ster">Konsistenz:</p>
            </div>{ausscheidungen.length === 0 ? (
                <p>Keine Daten vorhanden.</p>
            ) : (
                ausscheidungen.map((ausscheidung) => (
                    <div key={ausscheidung.id} className="AusscheidungWrap">
                        <p>{new Date(ausscheidung.time).toLocaleString()}</p>
                        <p>{ausscheidung.abstand}</p>
                        <p>{ausscheidung.menge}</p>
                        <p>{ausscheidung.konsistenz}</p>
                    </div>
                ))
            )}
        </div>
    );
}