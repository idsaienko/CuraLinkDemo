import { NavLink } from "react-router-dom";

export default function SidebarStaff() {
    const links = [
        { name: "Bewohner", path: "/staff/residents" },
        { name: "Berichte", path: "/staff/reports" },
        { name: "Aufgaben", path: "/staff/tasks" },
        { name: "Zeitplan", path: "/staff/schedule" },
        { name: "Dokumente", path: "/staff/documents" },
        { name: "Termine", path: "/staff/appointments" },
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
