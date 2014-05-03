#pragma once
#include <windows.h>
#include <iostream>
#include <memory>
#include <conio.h>

using Platform::String;

namespace NcxPppp
{
    public ref class LibRrrr sealed
    {
    public:
		LibRrrr();
		// virtual ~LibRrrr();
		String^ route(String^ path, int from, int to);
	private:
		std::unique_ptr<char> LibRrrr::PlatformStringToCharArray(String^ string);
		std::wstring ToWString(const char* utf8String, unsigned int length);
		String^ ToPlatformString(const char* utf8String, unsigned int length);
    };
}