import { NavLink, useParams } from "react-router-dom";

export default function SidebarResident() {
    const { id } = useParams();

    const linkClass = ({ isActive }: { isActive: boolean }) =>
        `block px-4 py-2 rounded hover:bg-gray-200 ${isActive ? "bg-gray-300 font-bold" : ""
        }`;

    return (
        <nav className="w-56 border-r h-full p-4">
            <ul className="space-y-2">
                <li><NavLink to={`/resident/${id}/overview`} className={linkClass}>Uebersicht</NavLink></li>
                <li><NavLink to={`/resident/${id}/reports`} className={linkClass}>Pflegeberichte</NavLink></li>
                <li><NavLink to={`/resident/${id}/medications`} className={linkClass}>Medikamente</NavLink></li>
                <li><NavLink to={`/resident/${id}/vitals`} className={linkClass}>Vitalwerte</NavLink></li>
                <li><NavLink to={`/resident/${id}/pain`} className={linkClass}>Schmerzen & Beobachtung</NavLink></li>
                <li><NavLink to={`/resident/${id}/nutrition`} className={linkClass}>Ernaehrung</NavLink></li>
                <li><NavLink to={`/resident/${id}/movement`} className={linkClass}>Mobilitaet</NavLink></li>
                <li><NavLink to={`/resident/${id}/excretion`} className={linkClass}>Ausscheidung</NavLink></li>
                <li><NavLink to={`/resident/${id}/documents`} className={linkClass}>Dokumente</NavLink></li>
                <li><NavLink to={`/resident/${id}/appointments`} className={linkClass}>Termine</NavLink></li>
            </ul>
        </nav>
    );
}
