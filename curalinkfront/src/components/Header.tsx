import { Search } from "lucide-react";
import { useLocation, useNavigate } from "react-router-dom";

/*type HeaderProps = {
    showNewEntry?: boolean;
    staff?: { name: string; photo: string };
};*/

export default function Header() {
    const location = useLocation();
    const navigate = useNavigate();
    const isResidentPage = location.pathname.startsWith("/resident/");
    const isStafftPage = location.pathname.startsWith("/staff/");
    return (
        <header className="Header">
            <div className="flex items-center gap-2"
                style={{
                    display: "inline-flex",
                    textAlign: "center"
                }}>
                <img src="/logo.jpg" alt="logo" className="w-8 h-8 Logo"/>
                <h2 className="CuraLink">CuraLink</h2>
            </div>

            <div className="flex items-center border rounded-xl px-3 py-2 SearchbarDiv">
                <Search className="w-5 h-5 text-gray-400" />
                <input
                    type="text"
                    placeholder="Suche Bewohner oder Berichte..."
                    className="ml-2 flex-1 outline-none Searchbar"
                    style={{
                        border: "2px",
                        borderColor: "black",
                        color: "black"
                    }}
                />
            </div>

            {isResidentPage && (
                <button
                    onClick={() => navigate("/staff/dashboard")}
                    className="ReportButton">
                    + Neuer Eintrag
                </button>
            )}
            {isStafftPage && (
                <div className="StaffHeader">
                    <img
                        src="/staff.jpg"
                        alt="staff"
                        className="StaffFoto"/>
                    <span className="StaffName">Peter M.</span>
                </div>
            )}
        </header>
    );
}
