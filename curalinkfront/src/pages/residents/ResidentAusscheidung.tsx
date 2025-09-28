import { useEffect, useState } from "react";
import api from "@/services/api";

export default function ResidentAusscheidung({ residentId }: { residentId: number }) {
    const [entries, setEntries] = useState<any[]>([]);

    useEffect(() => {
        api.get(`/ausscheidung/${residentId}`).then(res => setEntries(res.data));
    }, [residentId]);

    return (
        <div className="p-4">
            <h2 className="text-xl font-bold mb-4">Ausscheidung</h2>
            <ul className="space-y-2">
                {entries.map((e, i) => (
                    <li key={i} className="border p-2 rounded">
                        <p><b>Zeit:</b> {new Date(e.time).toLocaleString()}</p>
                        <p><b>Abstand:</b> {e.abstand}</p>
                        <p><b>Menge:</b> {e.menge}</p>
                        <p><b>Konsistenz:</b> {e.konsistenz}</p>
                        <p><b>Erfasst von:</b> {e.staff?.name}</p>
                    </li>
                ))}
            </ul>
        </div>
    );
}
