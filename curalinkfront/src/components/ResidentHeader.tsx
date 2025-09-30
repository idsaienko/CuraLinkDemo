import { useEffect, useState } from "react";
import { getResident } from "@/services/api";

interface Resident {
    id: number;
    name: string;
    room: string;
    photoUrl?: string;
}

export default function ResidentHeader({ residentId }: { residentId: number }) {
    const [resident, setResident] = useState<Resident | null>(null);

    useEffect(() => {
        getResident(residentId).then(setResident).catch(() => setResident(null));
    }, [residentId]);

    if (!resident) {
        return <div className="p-6 text-gray-500">Lade Bewohnerdaten...</div>;
    }

    return (
        <div className="flex items-center space-x-6 p-6 border-b border-gray-200 bg-white shadow-sm rounded-lg mb-6"
            style={{
                color:"black"
            }}>
            <img
                src={resident.photoUrl || "/default-avatar.png"}
                alt={resident.name}
                className="h-20 w-20 rounded-full object-cover border border-gray-300"
            />

            <div>
                <h1 className="text-2xl font-bold">{resident.name}</h1>
                <p className="text-gray-600">{resident.room}</p>
            </div>
        </div>
    );
}
