WINDOWS UNITY3D LZMA SPEEDUP
=============================

How to use
----------
1. Rename your C:\Program Files (x86)\Unity\Editor\Data\Tools\lzma.exe to lzma_real.exe. You can also make a separate backup to a safe place if you wish.
2. Copy lzma.exe from the "windows binaries" directory (or your custom build from sources) into your Unity3D tools folder
3. -Done-

How does it work?
-----------------
Whenever Unity3D uses LZMA compression, the "fake" lzma.exe from this Speedup Project will substitute Unity3Ds max-compression parameters (for e.g. -fb372) to those which provide fastest compression (-a0 -d0 -mt4 -fb5 -mc0 -lc0 -pb0 -mfbt2).

This interceptor can speed up compressing the build by a factor of 4!

---> Important: Speed don't come for free: Compression rate will be worst, only use it in your local development environment! <---

Unity3D Versions
----------------------------
Tested with Unity 3.4 and Unity 3.5 on Windows

News
----
Follow me on Twitter @[derFunk]! Let me know if this little project helped you saving time, and if you experienced even better speedup factors.

[derFunk]: http://twitter.com/derFunk