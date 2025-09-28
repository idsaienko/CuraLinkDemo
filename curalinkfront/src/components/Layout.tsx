import { Outlet } from "react-router-dom";
import Header from "./Header";
import Sidebar from "./SidebarResident";

export default function Layout() {

    return (
        <div className="h-screen flex flex-col">
            <Header showNewEntry={true} />
            <div className="flex flex-1">
                <Sidebar />
                <main className="flex-1 p-6 bg-gray-100 overflow-auto">
                    <Outlet />
                </main>
            </div>
        </div>
    );
}
