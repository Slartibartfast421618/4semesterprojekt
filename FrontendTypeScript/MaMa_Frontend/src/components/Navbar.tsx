import React from "react";
import mamaLogo from "../assets/manemanager_nbg_close.png";

type NavbarProps = {
    place: string;
    start: string;
    end: string;
    onPlaceChange: (v: string) => void;
    onStartChange: (v: string) => void;
    onEndChange: (v: string) => void;
};

export default function Navbar({
    place,
    start,
    end,
    onPlaceChange,
    onStartChange,
    onEndChange,
}: NavbarProps) {
    return (
        <div className="navbar">
            <a href="http://localhost:7000" target="_blank" rel="noreferrer">
                <img src={mamaLogo} className="logo" alt="Mane Manager logo" />
            </a>

            <div className="stack textbox">
                <span className="name">Mane Manager</span>
                <span className="slogan">Mane management made mild</span>
            </div>

            <div className="spacer" />

            <div className="stack">
                <span className="searchterm">Place</span>
                <input
                    className="searchinput"
                    value={place}
                    onChange={(e) => onPlaceChange(e.target.value)}
                    placeholder="Search area"
                />
            </div>

            <div className="stack">
                <span className="searchterm">DateOnly start</span>
                <input
                    className="searchinput"
                    value={start}
                    onChange={(e) => onStartChange(e.target.value)}
                    placeholder="YYYY-MM-DD"
                />
            </div>

            <div className="stack">
                <span className="searchterm">DateOnly end</span>
                <input
                    className="searchinput"
                    value={end}
                    onChange={(e) => onEndChange(e.target.value)}
                    placeholder="YYYY-MM-DD"
                />
            </div>

            <div className="spacer" />
        </div>
    );
}