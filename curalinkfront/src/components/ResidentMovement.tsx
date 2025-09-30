import { useEffect, useState } from "react";
import { getMovements, type Movement } from "@/services/movementService";

export default function ResidentMovement({ residentId }: { residentId: number }) {
    const [movements, setMovements] = useState<Movement[]>([]);

    useEffect(() => {
        getMovements(residentId).then(setMovements);
    }, [residentId]);

    return (
        <div>
            <h2 className="text-xl font-bold mb-4">Mobilität</h2>
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
                            <p><strong>Notizen:</strong> {movement.notes}</p>
                        </li>
                    ))}
                </ul>
            )}
        </div>
    );
}
