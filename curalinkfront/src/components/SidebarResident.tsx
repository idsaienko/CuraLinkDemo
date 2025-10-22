import { NavLink } from "react-router-dom";
import { useParams } from "react-router-dom";

export default function SidebarResident() {

    const linkClass = ({ isActive }: { isActive: boolean }) =>
        `block px-4 py-2 rounded hover:bg-gray-200 ${isActive ? "bg-gray-300 font-bold" : ""
        }`;

    const { residentId } = useParams<{ residentId: string }>();

    return (
        <nav className="w-56 border-r h-full p-4">
            <ul className="space-y-2">
                <li><NavLink to={`/resident/${residentId}/overview`} className={linkClass}>Uebersicht</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/reports`} className={linkClass}>Pflegeberichte</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/medications`} className={linkClass}>Medikamente</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/vitals`} className={linkClass}>Vitalwerte</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/pain`} className={linkClass}>Schmerzen & Beobachtung</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/movement`} className={linkClass}>Mobilitaet</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/nutrition`} className={linkClass}>Ernaehrung</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/ausscheidung`} className={linkClass}>Ausscheidung</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/documents`} className={linkClass}>Dokumente</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/appointments`} className={linkClass}>Termine</NavLink></li>
            </ul>
        </nav>
    );
}
