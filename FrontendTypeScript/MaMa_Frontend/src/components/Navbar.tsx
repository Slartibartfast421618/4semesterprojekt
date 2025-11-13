import React from "react";
import mamaLogo from "../assets/manemanager_nbg_close.png";

type NavbarProps = {
    place: string;
    dateEarliest: string;
    dateLatest: string;
    onPlaceChange: (v: string) => void;
    onDateEarliestChange: (v: string) => void;
    onDateLatestChange: (v: string) => void;
};

export default function Navbar({
    place,
    dateEarliest,
    dateLatest,
    onPlaceChange,
    onDateEarliestChange,
    onDateLatestChange,
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
                <span className="searchterm">DateStart</span>
                <input
                    className="searchinput"
                    value={dateEarliest}
                    onChange={(e) => onDateEarliestChange(e.target.value)}
                    placeholder="YYYY-MM-DD"
                />
            </div>

            <div className="stack">
                <span className="searchterm">DateEnd</span>
                <input
                    className="searchinput"
                    value={dateLatest}
                    onChange={(e) => onDateLatestChange(e.target.value)}
                    placeholder="YYYY-MM-DD"
                />
            </div>

            <div className="spacer" />
        </div>
    );
}