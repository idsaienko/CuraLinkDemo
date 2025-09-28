import { NavLink } from "react-router-dom";

export default function SidebarResident() {
    const links = [
        { name: "Übersicht", path: "/" },
        { name: "Pflegeberichte", path: "/reports" },
        { name: "Medikamente", path: "/medications" },
        { name: "Vitalwerte", path: "/vitals" },
        { name: "Dokumente", path: "/documents" },
        { name: "Schmerzen & Beobachtung", path: "/pain" },
        { name: "Termine", path: "/appointments" },
        { name: "Ausscheidung", path: "/auscheidung" },
    ];

    return (
        <aside className="w-64 bg-gray-50 border-r p-4">
            <nav className="flex flex-col gap-3">
                {links.map((link) => (
                    <NavLink
                        key={link.path}
                        to={link.path}
                        className={({ isActive }) =>
                            `block px-3 py-2 rounded-lg ${isActive
                                ? "bg-blue-100 text-blue-700 font-semibold"
                                : "text-gray-700 hover:bg-gray-100"
                            }`
                        }
                    >
                        {link.name}
                    </NavLink>
                ))}
            </nav>
        </aside>
    );
}
