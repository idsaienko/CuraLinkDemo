import { useParams } from "react-router-dom";
import { useResident } from "../hooks/useResident";

export default function ResidentHeader() {
    const { residentId } = useParams<{ residentId: string }>();
    const { resident, loading } = useResident(residentId);

    if (loading) return <p>Lädt...</p>;
    if (!resident) return <p>Kein Bewohner gefunden</p>;

    return (
        <div className="flex items-center gap-4 mb-4">
            <img
                src={resident.photoUrl || "/placeholder.jpg"}
                alt={resident.fullName}
                className="w-20 h-20 rounded-full object-cover"
            />
            <div>
                <h1 className="text-2xl font-bold">{resident.fullName}</h1>
                <p className="text-gray-600">{resident.roomNumber}</p>
            </div>
        </div>
    );
}
