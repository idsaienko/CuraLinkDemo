import { NavLink } from "react-router-dom";
import { useParams } from "react-router-dom";

export default function SidebarResident() {

    const { residentId } = useParams<{ residentId: string }>();

    return (
        <nav className="w-56 border-r h-full p-4">
            <ul className="space-y-2 LinkList">
                <li><NavLink to={`/resident/${residentId}/overview`} className="SidebarLink">Uebersicht</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/reports`} className="SidebarLink">Pflegeberichte</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/medications`} className="SidebarLink" >Medikamente</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/vitals`} className="SidebarLink">Vitalwerte</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/pain`} className="SidebarLink">Schmerzen & Beobachtung</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/movement`} className="SidebarLink">Mobilitaet</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/nutrition`} className="SidebarLink">Ernaehrung</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/ausscheidung`} className="SidebarLink">Ausscheidung</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/documents`} className="SidebarLink">Dokumente</NavLink></li>
                <li><NavLink to={`/resident/${residentId}/appointments`} className="SidebarLink">Termine</NavLink></li>
            </ul>
        </nav>
    );
}
