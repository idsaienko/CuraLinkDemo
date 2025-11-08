import { Outlet, useLocation } from "react-router-dom";
import Header from "./Header";
import SidebarResident from "./SidebarResident";

export default function Layout() {

    const location = useLocation();
    const isResidentPage = location.pathname.startsWith('/resident/');

    return (
        <div className="Container">
            <Header showNewEntry={true} />
            <div className="BodyContainer">
                {isResidentPage && <SidebarResident />}
                <main className="flex-1 p-6 bg-gray-100 overflow-auto">
                    <Outlet />
                </main>
            </div>
        </div>
    );
}
