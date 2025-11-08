import { useParams } from "react-router-dom";
import { useResident } from "../hooks/useResident";

export default function ResidentHeader() {
    const { residentId } = useParams<{ residentId: string }>();
    const { resident, loading } = useResident(residentId);

    if (loading) return <p className = "message">Lädt...</p>;
    if (!resident) return <p className = "message">Kein Bewohner gefunden</p>;

    return (
        <div className="ResHeaderWrap">
            <img
                src={resident.photoUrl || "/img/placeholder.png"}
                alt={resident.fullName}
                className="ResImg"
            />
            <div>
                <h1>{resident.fullName}</h1>
                <p>{resident.roomNumber}</p>
            </div>
        </div>
    );
}
