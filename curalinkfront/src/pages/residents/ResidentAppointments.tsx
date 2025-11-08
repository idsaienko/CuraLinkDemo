import { useParams } from "react-router-dom";
import { useResident } from "@/hooks/useResident";

export default function ResidentAppointmentsPage() {
    const { residentId } = useParams<{ residentId: string }>();
    const { resident, loading } = useResident(residentId);

    if (loading) return <p>Lädt...</p>;
    if (!resident) return <p>Kein Bewohner gefunden</p>;

    return (
        <div className="p-4" style={{color:"black"}}>
            <h2 className="text-2xl font-bold">Termine von {resident.fullName}</h2>
            <div>
                <p><strong>{resident.appointment.type}</strong> – {new Date(resident.appointment.dateTime).toLocaleTimeString()}</p>
                <p>Arzt: {resident.appointment.notes}</p>
            </div>
        </div>
    );
}