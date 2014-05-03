// NcxPppp.cpp
#include "NcxPppp.h"

extern "C" {
	#include "config.h"
	#include "tdata.h"
	#include "router.h"
}
using namespace NcxPppp;
using namespace Platform;
using Platform::String;

namespace NcxPppp
{

	LibRrrr::LibRrrr() {	
		//Platform::Details::Debug::WriteLine("Blah1!");
		OutputDebugString(L"Init\n");
	}
	
	Platform::String^ LibRrrr::route(Platform::String^ path, int from, int to) {
		OutputDebugString(L"Doing routing\n");
		tdata_t tdata;
		auto pathConverted = PlatformStringToCharArray(path);
		OutputDebugString(L"Loadeing timetable\n");
		tdata_load(pathConverted.get(), &tdata);
		OutputDebugString(L"Loaded timetable!\n");
		router_request_t req;
		router_request_initialize(&req);
		router_request_randomize(&req, &tdata);
		req.from = from;
		req.to = to;

		router_t router;
		router_setup(&router, &tdata);
		router_route(&router, &req);
		char result_buf[OUTPUT_LEN];
		router_result_dump(&router, &req, result_buf, OUTPUT_LEN);

		// Debug
		int size = MultiByteToWideChar(CP_ACP, 0, result_buf, -1, NULL, 0);
		if (size > 0) {
			WCHAR* message = new WCHAR[size];
			size = MultiByteToWideChar(CP_ACP, 0, result_buf, -1, message, size);
			OutputDebugString(message);
			delete[] message;
		}
		return ToPlatformString(result_buf, OUTPUT_LEN);
	}

	std::unique_ptr<char> LibRrrr::PlatformStringToCharArray(String^ string) {
		auto wideData = string->Data();
		int bufferSize = string->Length() + 1;
		char* ansi = new char[bufferSize];
		WideCharToMultiByte(CP_UTF8, 0, wideData, -1, ansi, bufferSize, NULL, NULL);
		return std::unique_ptr<char>(ansi); 
	}

	std::wstring LibRrrr::ToWString(const char* utf8String, unsigned int length) {
		DWORD numCharacters = MultiByteToWideChar(CP_UTF8, 0, utf8String, length, nullptr, 0);
		auto wideText = new std::wstring::value_type[numCharacters];
		MultiByteToWideChar(CP_UTF8, 0, utf8String, length, wideText, numCharacters);
		std::wstring result(wideText);
		delete[] wideText;
		return result;
	}

	Platform::String^ LibRrrr::ToPlatformString(const char* utf8String, unsigned int length) {
		return ref new Platform::String(ToWString(utf8String, length).data());
	}
}
