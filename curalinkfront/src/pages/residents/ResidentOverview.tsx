import { useParams } from "react-router-dom";
import { useResident } from "@/hooks/useResident";
import ResidentHeader from "../../components/ResidentHeader";

export default function ResidentOverview() {
    const { residentId } = useParams<{ residentId: string }>();
    const { resident, loading } = useResident(residentId);

    if (loading) return <p>L�dt...</p>;
    if (!resident) return <p>Kein Bewohner gefunden</p>;

    return (
        <div className="space-y-6"
            style={{
                color:"black"
            } }>
            <ResidentHeader />

            <div className="grid grid-cols-3 gap-6">
                <div className="bg-white p-4 rounded-xl shadow">
                    <h3 className="font-semibold mb-2">Allgemeine Infos</h3>
                    <ul className="text-sm text-gray-700 space-y-1">
                        <li>{resident.location}</li>
                        <li>Diaet: Vegetarisch</li>
                        <li>Kontakt: +49 123 456789</li>
                    </ul>
                </div>

                <div className="bg-white p-4 rounded-xl shadow">
                    <h3 className="font-semibold mb-2">Tagesplan</h3>
                    <ul className="space-y-2">
                        <li className="flex items-center gap-2">
                            <span className="w-3 h-3 rounded-full bg-green-500"></span>
                            08:00 Fr�hst�ck � erledigt
                        </li>
                        <li className="flex items-center gap-2">
                            <span className="w-3 h-3 rounded-full bg-yellow-400"></span>
                            12:00 Mittagessen � ausstehend
                        </li>
                    </ul>
                </div>

                <div className="bg-white p-4 rounded-xl shadow">
                    <h3 className="font-semibold mb-2">Letzte Aktivit�ten</h3>
                    <ul className="text-sm space-y-1">
                        <li>Gestern: Schmerzmittel verabreicht</li>
                        <li>Gestern: Spaziergang im Garten</li>
                    </ul>
                </div>
            </div>
        </div>
    );
}
