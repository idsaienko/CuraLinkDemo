import { Search } from "lucide-react";

type HeaderProps = {
    showNewEntry?: boolean;
    staff?: { name: string; photo: string };
};

export default function Header({ showNewEntry, staff }: HeaderProps) {
    return (
        <header className="w-full bg-white shadow flex items-center justify-between px-6 py-3"
            style={{
                display: "inline-flex",
                top: "0px",
                left: "0px"

            }}>
            <div className="flex items-center gap-2"
                style={{
                    display: "inline-flex",
                    textAlign: "center"}}>
                <img src="/logo.jpg" alt="logo" className="w-8 h-8" style={{height:"33px"}} />
                <h2 style={{ color: "#008E56", height: "30px", margin:"0px", fontSize:"25px" }}
                    className="text-xl color-dark-green font-bold text-blue-700">CuraLink</h2>
            </div>

            <div className="flex-1 mx-10">
                <div className="flex items-center border rounded-xl px-3 py-2">
                    <Search className="w-5 h-5 text-gray-400" />
                    <input
                        type="text"
                        placeholder="Suche Bewohner oder Berichte..."
                        className="ml-2 flex-1 outline-none"
                        style={{
                            border: "2px",
                            borderColor: "black",
                            color: "black",

                        }}
                    />
                </div>
            </div>

            {showNewEntry && (
                <button className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700"
                    style={{ color:"#008E56"}}
                >
                    + Neuer Eintrag
                </button>
            )}
            {staff && (
                <div className="flex items-center gap-2"
                    style={{}}>
                    <span className="font-medium" defaultValue="Peter M.">{staff.name}</span>
                    <img
                        src={staff.photo}
                        alt="staff"
                        className="w-10 h-10 rounded-full object-cover"
                        style={{width: "47px", height:"47px"}}
                    />
                </div>
            )}
        </header>
    );
}
