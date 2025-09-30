import { useEffect, useState } from "react";
import { getAusscheidungen, type Ausscheidung } from "@/services/ausscheidungService";

export default function ResidentAusscheidung({ residentId }: { residentId: number }) {
    const [ausscheidungen, setExcretions] = useState<Ausscheidung[]>([]);

    useEffect(() => {
        getAusscheidungen(residentId).then(setExcretions);
    }, [residentId]);

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
