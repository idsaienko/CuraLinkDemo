import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Layout from "./components/Layout";
import ResidentOverview from "./pages/residents/ResidentOverview";
import SidebarStaff from "./components/SidebarStaff";
import StaffDashboard from "./pages/staff/StaffDashboard";
import Header from "./components/Header";
import ResidentAusscheidung from "./pages/residents/ResidentAusscheidung";

function App() {

    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Navigate to="/staff" replace />} />

                <Route path="/resident/:id" element={<Layout />}>
                    <Route index element={<ResidentOverview />} />
                </Route>

                <Route
                    path="/staff"
                    element={
                        <div className="h-screen flex flex-col"
                            style={{
                                position: "relative",
                                top: "0px",
                                left:"0px"
                            } }>
                            <Header staff={{ name: "Maria Müller", photo: "/staff.jpg" }} />
                            <div className="flex flex-1">
                                <SidebarStaff />
                                <StaffDashboard />
                            </div>
                        </div>
                    }
                />

                <Route path="/resident/:id/ausscheidung" element={<ResidentAusscheidung residentId={0} />} />
            </Routes>
        </BrowserRouter>
    );
}

export default App;
