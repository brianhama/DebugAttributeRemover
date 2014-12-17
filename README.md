DebugAttributeRemover
=====================

The Windows Phone and Windows 8.1 certification requirements were modified with the latest release. You may no longer submit an app using any assembly built in 'debug' mode. Unfortunately, the only available version of many third party libraries were built in 'debug' mode and the original developer has moved on from the project - never having released the source code!

This simple command line tool will remove the DebuggableAttribute from a given assembly.  This allows it to be used in an app which will be submitted to the Windows Phone or Windows Store.
